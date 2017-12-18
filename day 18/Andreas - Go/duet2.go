package main

import (
	"fmt"
	"strings"
	"strconv"
	"io/ioutil"
)

type prog struct{
	registers map[string]int
	lastValue int	
}

//register read & writes
func (p *prog) snd(x string){ 
	p.lastValue = p.registers[x] 
}
func (p *prog) set(x string, y string){
	if val, err := strconv.Atoi(y) ; err == nil {
		p.registers[x] = val	
	} else {
		p.registers[x] = p.registers[y]
	}
}
func (p *prog) add(x string, y string){
	if val, err := strconv.Atoi(y) ; err == nil {
		p.registers[x] += val
	} else {
		p.registers[x] += p.registers[y]
	}
}
func (p *prog) mul(x string, y string){
	if val, err := strconv.Atoi(y) ; err == nil {
		p.registers[x] *= val	
	} else {
		p.registers[x] *= p.registers[y]
	}
}
func (p *prog) mod(x string, y string){ 
	if val, err := strconv.Atoi(y) ; err == nil {
		p.registers[x] %= val	
	} else {
		p.registers[x] %= p.registers[y]
	}
}
func (p *prog) jgz(x string)int { //reg value or value on its own ?
	if val, err := strconv.Atoi(x) ; err == nil {
		return val
	} else {
		return p.registers[x]
	}
}

func NewProg() *prog{
	return &prog{map[string]int{}, 0}
}

func main() {
	//input := readInput("testInput.txt")
	input := readInput("input.txt")
	
	instructions := *parseInstructions(input)
	pos := 0
	prog := *NewProg()
	for pos < len(instructions) && pos >= 0 {

		instr := strings.Split(instructions[pos], " ")

		switch instr[0] {
			case "snd": prog.snd(instr[1])
			case "set": prog.set(instr[1],instr[2])
			case "add": prog.add(instr[1],instr[2])
			case "mul": prog.mul(instr[1],instr[2])
			case "mod": prog.mod(instr[1],instr[2])
			case "rcv": 
				if prog.jgz(instr[1]) != 0 {
					fmt.Println(prog.lastValue)
					panic(nil) //fuckit lets quit !
				}
			case "jgz":
				if prog.jgz(instr[1]) > 0 {
					pos += prog.jgz(instr[2]) - 1 //pos++ comes later => -1
				}
		}
		
		pos++
	}
}


func parseInstructions(input string) *[]string{
	res := strings.Split(input, "\n")
	return &res
}
func readInput(fname string) string {
	s, _ := ioutil.ReadFile(fname)
	return string(s)
}