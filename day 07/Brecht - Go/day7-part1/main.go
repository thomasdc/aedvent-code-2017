package main

import (
	"fmt"
)

type Node struct {
	Weight   int
	Children []Node
}

type WeightAndChildren struct {
	Weight   int
	Children []string
}

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func main() {
	allNodes, allChildren, mapNodeToWeightAndChildren := ReadAndParseFile("data.txt")
	nodeName := findRoot(allNodes, allChildren, mapNodeToWeightAndChildren)
	fmt.Println(nodeName)
}

func findRoot(allNodes StringSlice, allChildren StringSlice, mapNodeToWeightAndChildren map[string]WeightAndChildren) string {
	for _, nodeName := range allNodes {
		if checkIfRoot(nodeName, allChildren, mapNodeToWeightAndChildren) {
			return nodeName
		}
	}
	panic("there should be a root node")
}

func checkIfRoot(nodeName string, allChildren StringSlice, mapNodeToWeightAndChildren map[string]WeightAndChildren) bool {
	_, hasChildren := mapNodeToWeightAndChildren[nodeName]
	isChild := allChildren.Contains(nodeName)
	if hasChildren && !isChild {
		return true
	} else {
		return false
	}
}
