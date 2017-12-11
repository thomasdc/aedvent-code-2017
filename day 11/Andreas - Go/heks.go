package main
//http://keekerdc.com/2011/03/hexagon-grids-coordinate-systems-and-distance-calculations/


import (
	"fmt"
	"strings"
	"io/ioutil"
)

var directions = []string{
	"n","ne","nw",
	"s","sw","se",
}

var movesX = map[string]int { "n":  0, "s":  0, "ne":  1, "se":  1, "sw": -1, "nw": -1 }
var movesY = map[string]int { "n":  1, "s": -1, "ne":  1, "se":  0, "sw": -1, "nw":  0 }


func main(){
	input := readInput("input.txt")
	fmt.Println( Hexed(*input) )
}

func Hexed(s string) int{

	path := map[string]int{}
	for _, step := range steps(s){
		path[step]++
	}

	fmt.Println(path)

	return pathLength(&path)
}

func steps(s string) []string{
	return strings.Split(s, ",")
}
func pathLength(m *map[string]int) int{
	
	path := *m

	x,y := 0, 0
	for _, d := range directions{

		dx, dy := movesX[d], movesY[d]

		x += dx*path[d]
		y += dy*path[d]
	}
	
	return max(abs(y), abs(x))
}
func readInput(fname string) *string {
	s, _ := ioutil.ReadFile(fname)
	res := string(s)
	return &res
}
func abs(i int) int{
	if i < 0 { i = -i }
	return i
}
func max(a,b int) int{
	if a>b { return a }
	return b
}