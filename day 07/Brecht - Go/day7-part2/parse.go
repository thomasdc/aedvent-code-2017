package main

import (
	"bufio"
	"os"
	"regexp"
	"strconv"
	"strings"
)

type NodeAndChildren struct {
	node     string
	weight   int
	children []string
}

func ReadAndParseFile(p string) (StringSlice, StringSlice, map[string]NodeAndChildren) {
	file, err := os.Open(p)
	if err != nil {
		panic(err)
	}
	defer file.Close()

	scanner := bufio.NewScanner(file)
	treeNodeToChildren := make(map[string]NodeAndChildren)
	allChildren := StringSlice(make([]string, 0, 10))
	allNodes := StringSlice(make([]string, 0, 10))

	for scanner.Scan() {
		weight, name, children := parseLine(scanner.Text())
		allNodes.Append(name)
		for _, child := range children {
			if len(child) != 0 { // ignore empty lines
				allChildren.Append(child)
				allNodes.Append(name)
			}
		}
		treeNodeToChildren[name] = NodeAndChildren{name, weight, children}
	}

	return allNodes, allChildren, treeNodeToChildren
}

func parseLine(s string) (int, string, []string) {
	reg := regexp.MustCompile("\\((\\d+)\\)")
	numberStr := reg.FindString(s)
	numberStr = strings.Replace(numberStr, "(", "", -1)
	numberStr = strings.Replace(numberStr, ")", "", -1)
	weight, err := strconv.Atoi(numberStr)
	check(err)
	split := reg.Split(s, 2)
	name := strings.Replace(split[0], " ", "", -1)
	children := split[1]
	children = strings.Replace(children, " ", "", -1)
	if len(children) > 0 {
		children = strings.Replace(children, "->", "", -1)
		reg = regexp.MustCompile(",")
		return weight, name, reg.Split(children, -1)
	} else {
		return weight, name, []string{}
	}

}
