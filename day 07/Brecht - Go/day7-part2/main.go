package main

import (
	"fmt"
)

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func main() {
	allNodes, allChildren, mapNodeToWeightAndChildren := ReadAndParseFile("data.txt")
	rootNodeName := findRoot(allNodes, allChildren, mapNodeToWeightAndChildren)
	tree := BuildTree(rootNodeName, mapNodeToWeightAndChildren)
	tree.CalculateTotalNodeSums()
	tree.DrawTree()
	node, weight, _, _ := tree.FindOutOfBalanceNode()
	fmt.Println(node.name, node.weight, node.totalWeight, weight)

	node.weight = 1072
	tree.CalculateTotalNodeSums()
	node, levelWeight, lvl, balanced := tree.FindOutOfBalanceNode()
	if balanced {
		fmt.Println("oh my god this works!", node, lvl, levelWeight, balanced)
	}

}

func findRoot(allNodes StringSlice, allChildren StringSlice, mapNodeToWeightAndChildren map[string]NodeAndChildren) string {
	for _, nodeName := range allNodes {
		if checkIfRoot(nodeName, allChildren, mapNodeToWeightAndChildren) {
			return nodeName
		}
	}
	panic("there should be a root node")
}

func checkIfRoot(nodeName string, allChildren StringSlice, mapNodeToWeightAndChildren map[string]NodeAndChildren) bool {
	_, hasChildren := mapNodeToWeightAndChildren[nodeName]
	isChild := allChildren.Contains(nodeName)
	if hasChildren && !isChild {
		return true
	} else {
		return false
	}
}
