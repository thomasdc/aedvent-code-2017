package main

import (
	"fmt"
	"strings"
)

type Tree struct {
	leveltoNodes map[int]*TreeNodeSlice
	nameToNode   map[string]*TreeNode
	depth        int
	rootNode     *TreeNode
	size         int
}

type TreeNode struct {
	children    TreeNodeSlice
	name        string
	weight      int
	level       int
	totalWeight int
}

type TreeNodeSlice []*TreeNode

// Push adds a node to the stack.
func (sRef *TreeNode) AppendChild(n *TreeNode) {
	sRef.children.append(n)
}

func BuildTree(rootNodeName string, mapNameToNode map[string]NodeAndChildren) *Tree {
	// Init root node
	parsedNode := mapNameToNode[rootNodeName]
	rootTreeNode := TreeNode{[]*TreeNode{}, parsedNode.node, parsedNode.weight, 0, -1}
	tree := Tree{make(map[int]*TreeNodeSlice), make(map[string]*TreeNode), 0, &rootTreeNode, 0}
	(&tree).addNodeToLevel(&rootTreeNode)
	// Init stack element (we use a stack to avoid recursion... no tail recursion optimisation in go)
	toTraverse := CreateStack()
	toTraverse.Push(&rootTreeNode)

	// Traverse rest of tree
	for toTraverse.Size() > 0 {
		parentTreeNode := toTraverse.Pop().(*TreeNode)
		parsedNode := mapNameToNode[parentTreeNode.name]
		for _, childNodeName := range parsedNode.children {
			childParsedNode := mapNameToNode[childNodeName]
			treeNode := TreeNode{[]*TreeNode{}, childNodeName, childParsedNode.weight, parentTreeNode.level + 1, -1}
			toTraverse.Push(&treeNode)
			parentTreeNode.AppendChild(&treeNode)
			(&tree).addNodeToLevel(&treeNode)
		}

	}
	return &tree
}

func (tree *Tree) CalculateTotalNodeSums() {
	for lvl := tree.depth; lvl >= 0; lvl-- {
		nodes := tree.leveltoNodes[lvl]
		for _, node := range *nodes {
			totalWeight := node.weight
			for _, c := range node.children {
				totalWeight += c.totalWeight
			}
			node.totalWeight = totalWeight
		}
	}
}

func (tree *Tree) FindOutOfBalanceNode() (*TreeNode, []int, int, bool) {
	levels := tree.depth // start at deeper levels
	for lvl := levels; lvl >= 0; lvl-- {
		nodes := tree.leveltoNodes[lvl]
		for _, node := range *nodes {
			totalWeightCounts := make(map[int]int)
			for _, c := range node.children {
				totalWeightCounts[c.totalWeight] += 1
			}
			keys := make([]int, 0, len(totalWeightCounts))
			for k := range totalWeightCounts {
				keys = append(keys, k)
			}
			if len(keys) > 2 {
				panic("yeah well.. fix it yourself!")
			}

			if len(keys) > 1 {
				for _, c := range node.children {
					if totalWeightCounts[c.totalWeight] == 1 {
						return c, keys, node.level, false
					}
				}
			}
		}
	}
	return nil, []int{}, -1, true
}

func (tRef *Tree) addNodeToLevel(node *TreeNode) {
	if node.level >= tRef.depth {
		tRef.depth = node.level
	}
	tRef.size = tRef.size + 1
	_, hasLevel := tRef.leveltoNodes[node.level]
	if !hasLevel {
		nodeSlice := make(TreeNodeSlice, 0, 10)
		tRef.leveltoNodes[node.level] = &nodeSlice
	}
	tRef.nameToNode[node.name] = node
	nodeSlice := tRef.leveltoNodes[node.level]
	nodeSlice.append(node)
}

func (tree *Tree) DrawTree() {
	stack := CreateStack()
	stack.Push(tree.rootNode)
	for stack.Size() > 0 {
		node := stack.Pop().(*TreeNode)
		margin := strings.Repeat("  ", node.level)
		fmt.Printf("%v %v-%v (%v)[%v]\n", margin, node.level, node.name, node.weight, node.totalWeight)

		for _, c := range node.children {
			stack.Push(c)
		}
	}
}

func (sRef *TreeNodeSlice) doubleSlice() {
	s := *sRef
	c := cap(s)
	if c == 0 { // for slices that start with length 0
		c = 1
	}
	newSlice := make([]*TreeNode, len(s), 2*c)
	copy(newSlice, s)
	*sRef = newSlice
}

func (sRef *TreeNodeSlice) append(value *TreeNode) {
	s := *sRef
	n := len(s)
	if n == cap(s) {
		s.doubleSlice()
	}
	s = s[0 : n+1]
	s[n] = value
	*sRef = s
}
