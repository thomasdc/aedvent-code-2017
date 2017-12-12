package main

import (
	"strings"
	"strconv"
	"fmt"
	"io/ioutil"
)

func main(){
	input := readInput("input.txt")

	fmt.Println( InGroup(input) )
}


func InGroup(s string) int{
	graph := parseInput(s)
	res := connectedToZero(graph)
	return res
}

func parseInput(s string) *map[int]map[int]bool{
	res := map[int]map[int]bool{}
	for _, line := range strings.Split(s, "\n"){

		parts := strings.Split(line, " <-> ")
		
		from, _ := strconv.Atoi(parts[0])

		//init map
		if _, ex := res[from] ; !ex { res[from] = map[int]bool{} }
		
		//save the to values
		for _, t := range strings.Split(parts[1], ", ") {
			to, _ := strconv.Atoi(t)
			
			res[from][to] = true
		}

		
	}

	return &res
}
func connectedToZero(m *map[int]map[int]bool) int {
	graph := *m


	seen := map[int]bool{}
	
	todos := stack{}
	for k, _ := range graph[0] {
		todos.push(k)
	}


	for key := todos.pop() ; key > 0 ; key = todos.pop(){

		if seen[key] { continue }

		seen[key] = true
	
		for k, _ := range graph[key] {
			if !seen[k] { todos.push(k) }
		}

	}

	return len(seen) + 1 //+1 => node itself
}
func readInput(fname string) string {
	s, _ := ioutil.ReadFile(fname)
	return string(s)
}




type stack []int
func (s *stack) pop() int{
	l := len(*s)
	if l == 0 {
		return -1
	} else { 
		c := (*s)[l-1]
		*s = (*s)[:l-1] 
		return c
	}
}
func (s *stack) push(c int) {
	*s = append(*s, c)
}