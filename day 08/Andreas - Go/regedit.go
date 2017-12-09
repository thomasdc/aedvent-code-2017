package main

import (
	"strings"
	"strconv"
	"io/ioutil"
	"fmt"
)

func main(){
	input := readInput("input.txt")

	fmt.Println( Regedit(input) )
}

var registers = map[string]int{} //Sharing is caring !
func Regedit(s string) int{

	instructions := strings.Split(s, "\n")
	
	for _, instr := range instructions{
		handleInstruction(instr)
	}

	maxVal := 0
	for _, v := range registers{
		if v >= maxVal {
			maxVal = v
		}
	}

	return maxVal
}


var compiler = map[string]func(string, int)bool{
	//return here is useless
	"inc": func(reg string, value int)bool { 
		registers[reg] += value 
		return true
	},
	"dec": func(reg string, value int)bool { 
		registers[reg] -= value 
		return true
	},

	">"  : func(reg string, value int)bool { return registers[reg] >  value },
	">=" : func(reg string, value int)bool { return registers[reg] >= value },
	"<"  : func(reg string, value int)bool { return registers[reg] <  value },
	"<=" : func(reg string, value int)bool { return registers[reg] <= value },
	"==" : func(reg string, value int)bool { return registers[reg] == value },
	"!=" : func(reg string, value int)bool { return registers[reg] != value },
}
func handleInstruction(s string){
	parts := strings.Split(s, " ")

	ifVal, _ := strconv.Atoi(parts[6])
	inVal, _ := strconv.Atoi(parts[2])


	if compiler[parts[5]](parts[4], ifVal){
		compiler[parts[1]](parts[0], inVal)
	}
}

func readInput(fname string) string {
	s, _ := ioutil.ReadFile(fname)

	return string(s)
}