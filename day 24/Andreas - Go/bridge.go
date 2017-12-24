package main

import (
	"fmt"
	"io/ioutil"
	"strings"
	"strconv"
)

type Magnet struct{
	from, to int
}
func (m *Magnet) canConnectTo(v int) (bool, bool) {
	return m.from == v, m.to == v
}
func (m *Magnet) ToStringFromTo() string {
	return strconv.Itoa(m.from) + "/" + strconv.Itoa(m.to)
}
func (m *Magnet) ToStringToFrom() string {
	return strconv.Itoa(m.to) + "/" + strconv.Itoa(m.from)
}



func main() {
	magnets := parseInput(readInput("input.txt"))
	//magnets := parseInput(readInput("sampleInput.txt"))

	paths := searchPath(
		&Todo{
			start: 0,
			path : []Magnet{},
			used : make([]bool, len(*magnets)),
		},
		magnets,
	)
/*
	for i, p := range *paths{
		fmt.Print(i, " => 0")
		for _, m := range p {
			fmt.Print(" -> ", m)
		}
		fmt.Println()
	}
*/

	maxI, maxVal := 0, pathToValue( &(*paths)[0] )
	for i, p := range *paths {
		if val := pathToValue(&p) ; val > maxVal { 
			maxVal = val ; maxI = i 
		}
	}

	fmt.Println(maxI, maxVal)
	fmt.Print("0")
	for _, m := range (*paths)[maxI] {
		fmt.Print(" -> ", m)
	}
	fmt.Println()
	


}

func pathToString(p *[]Magnet) *string{
	res := ""
	for _, m := range *p {
		res += "{" + strconv.Itoa(m.from) + "-" + strconv.Itoa(m.to) + "}"
	}
	return &res
}
func pathToValue(p *[]Magnet) int{
	res := 0
	for _, m := range *p {
		res += m.from
		res += m.to
	}
	return res
}




func searchPath(start *Todo, magnets *[]Magnet) *[][]Magnet{

	paths := [][]Magnet{}
	todos := stack{start}
	seen  := map[string]bool{} //keep track of already discovered paths
	
	//generate all paths!
	for todo := todos.pop() ; todo != nil ; todo = todos.pop(){
		td := *todo

		for i, m := range *magnets {
			//skip used & useless magnets
			if td.used[i] { continue }
			conFrom, conTo := m.canConnectTo(td.start)
			if !conFrom && !conTo { continue }

			//create copies!
			newPath := make([]Magnet, len(td.path), len(td.path)+1)
			for j, v := range td.path { newPath[j] = v }
			newPath = append(newPath, m)

			pathString := *pathToString(&newPath)
			if seen[pathString]{ continue }

			paths = append(paths, newPath)
			seen[pathString] = true

			newUsed := make([]bool, len(td.used))
			for j, b := range td.used { newUsed[j] = b }
			newUsed[i] = true

			//keep looking
			if conFrom {
				todos.push(&Todo{
					start: m.to   , 
					path : newPath,
					used : newUsed,
				})
			}

			if conTo {
				todos.push(&Todo{
					start: m.from , 
					path : newPath,
					used : newUsed,
				})
			}
		}

		//fmt.Println("td: ",len(todos), "paths %d", len(paths))
	}

	return &paths
}

type Todo struct{
	start int
	path []Magnet
	used []bool
}
type stack []*Todo
func (s *stack) pop() *Todo{
	l := len(*s)
	if l == 0 {
		return nil
	} else { 
		c := (*s)[l-1]
		*s = (*s)[:l-1] 
		return c
	}
}
func (s *stack) push(c *Todo) { *s = append(*s, c) }

func parseInput(s string) *[]Magnet {
	lines := strings.Split(s, "\n")

	res := make([]Magnet, len(lines))
	for i, line := range lines {
		
		parts := strings.Split(line, "/")
		from, _ := strconv.Atoi(parts[0])
		to  , _ := strconv.Atoi(parts[1])

		res[i] = Magnet{
			from: from,
			to  : to  ,
		}
	}

	return &res
}
func readInput(fname string) string{
	res, _ := ioutil.ReadFile(fname)
	return string(res)
}