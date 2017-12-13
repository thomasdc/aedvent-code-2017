package main

import (
	"strings"
	"strconv"
	"fmt"
	"io/ioutil"
)

func main(){
	input := readInput("input.txt")

	fmt.Println( Severity(input) )

}

func Severity(input string) int{
	firewall,length := parseInput(input)

	severity := 0
	for i := 0; i <= length; i++ {
		

		layerDepth := firewall[i]
		if layerDepth == 0 { continue }

		scannerPos := i % layerDepth

		fmt.Println(i, layerDepth, scannerPos)
		if scannerPos == 0 {
			severity += i * (layerDepth+2)/2
		}
	}

	return severity
}

func parseInput(s string) (map[int]int, int){
	s = strings.Replace(s, " ", "", -1)
	max := -1

	firewall := map[int]int{}
	for _, line := range strings.Split(s, "\n") {
		parts := strings.Split(line, ":")
		
		i  , _ := strconv.Atoi(parts[0])
		val, _ := strconv.Atoi(parts[1])

		firewall[i]= val + val - 2

		if val > max { max = val }
		if i   > max { max = i }
	}

	return firewall, max
}
func readInput(fname string) string {
	s, _ := ioutil.ReadFile(fname)
	return string(s)
}