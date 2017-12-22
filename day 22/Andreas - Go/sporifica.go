package main

import (
	"fmt"
	"strings"
	"io/ioutil"
)

type direction int
const (
	U direction = iota
	R
	D
	L
)
var dxdy = map[direction]struct{dx, dy int}{
	U: {  0,  1},
	R: {  1,  0},
	D: {  0, -1},
	L: { -1,  0},
}

var example = `..#
#..
...`


type Field struct {
	nodes map[int]map[int]bool //nodes[y][x] = isInfected()
	x,y int
	dir direction
	ctr int
}

func main() {
		
	//example
	field := NewField()
	field.loadMap(example)

	fmt.Println(field) 
	field.print()
		
	for i := 0 ; i < 10000 ; i++ {
		field.step()
	}
	//field.print()
	fmt.Println(field.ctr)

	//part1
	field = NewField()
	field.loadMap( readInput("input.txt") )
	for i := 0; i < 10000; i++ { field.step() }
	fmt.Println(field.ctr)


}

func NewField() *Field{
	return &Field{
		nodes: map[int]map[int]bool{},
		x: 0,
		y: 0,
		dir: U,
	}
}
func (f *Field) step(){
	if f.nodes[f.y][f.x] {
		f.dir = (f.dir+1)%4
	} else {
		f.dir = (f.dir+3)%4
	}

	if _, ex := f.nodes[f.y] ; !ex { f.nodes[f.y] = map[int]bool{} }
	f.nodes[f.y][f.x] = !f.nodes[f.y][f.x]
	if f.nodes[f.y][f.x] { f.ctr++ }

	f.x += dxdy[f.dir].dx
	f.y += dxdy[f.dir].dy
}
func (f *Field) loadMap(s string){
	f.nodes = map[int]map[int]bool{}
	
	lines := strings.Split(s, "\n")
	offset := int(len(lines)/2)

	for j, y := 0, offset ; y >= -offset ; j,y = j+1, y-1 {
		line := lines[j]
		if _, ex := f.nodes[y] ; !ex { f.nodes[y] = map[int]bool{} }
		
		for i,x := 0, -offset ; x <= offset ; i,x = i+1, x+1 {
			f.nodes[y][x] = line[i] == '#'
		}
	}
}
func (f *Field) print(){
	r := int(len(f.nodes)/2) + 1

	for i := r ; i >= -r ; i-- {
		for j := -r ; j <= r ; j++ {

			if infected, ex := f.nodes[i][j] ; ex && infected {
				fmt.Print("#")
			} else {
				fmt.Print(".")
			}

		}
		fmt.Println()
	}
	fmt.Println()
}

func readInput(fname string) string {
	s, _ := ioutil.ReadFile(fname)
	return string(s)
}