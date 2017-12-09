package main

import (
	"fmt"
	"io/ioutil"
)


func main(){
	input := *readInput("input.txt")

	fmt.Println(ParseScore(input))
}

func ParseScore(s string) int{
	//clean strings
	s = removeCancels(s)

	lvl := 0
	score :=  0
	inGarbage := false
	for _, c := range s {

		if c == rune('{') && !inGarbage {
			lvl++
			score+= lvl
		} else if c == rune('}') && !inGarbage {
			lvl--
		} else if c == rune('<') {
			inGarbage = true
		} else if c == rune('>') {
			inGarbage = false
		}
	}

	return score
}
/*
func CollectGarbage(s string) int{
	s = removeCancels(s)

	ctr := 0 
	inGarbage := false

	for pos, c := range s {

		if c == rune('<') {
			inGarbage = true
		} else if c == rune('>') && pos > 0 && s[pos-1] != byte('!'){
			inGarbage = false
		}
	}

	return score
}
*/
func removeCancels(s string) string{
	res := ""
	for i := 0; i < len(s) ; i++{
		c := s[i]

		if c == byte('!') {
			i++ //skip 
		} else {
			res += string(c)
		}

	}

	return res
}

func readInput(fname string) *string {
	s, _ := ioutil.ReadFile(fname)
	res := string(s)
	return &res
}