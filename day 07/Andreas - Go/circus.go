package main

import (
	"strings"
	"fmt"
	"io/ioutil"
)

func main(){
	input := readInput("input.txt")

	fmt.Println( FindBottom(input) )
}


func FindBottom(s string) string{

	children := getChildren(s)
	//fmt.Println( *children )

	parents := getParents(children)
	//fmt.Println( *parents )

	return getRoot(parents, children)
}

//map[parent] = list of children
func getChildren(s string) *map[string][]string {
	children := map[string][]string{}

	s = strings.Replace(s, "," , "", -1)
	for _, line := range strings.Split(s, "\n"){

		parts := strings.Split(line, " ")

		if len(parts) > 3 {
			children[parts[0]] = append(children[parts[0]], parts[3:]...)
		}

	}
	return &children
}
//Reverse the map => map[child] = parent
func getParents(children *map[string][]string) *map[string]string {
	parents := map[string]string{}
	for parent, childs := range *children{
		for _, c := range childs{
			parents[c] = parent
		}
	}
	return &parents
}
//parent should have at least one child
//loop over nodes with children & check if their parent exists
func getRoot(parents *map[string]string, children *map[string][]string)string {

	//assumption parent has a child
	par, chi := *parents, *children
	for c := range chi{
		if _, ex := par[c] ; !ex { return c}
	}

	return ""
}


func readInput(fname string) string {
	s, _ := ioutil.ReadFile(fname)

	return string(s)
}