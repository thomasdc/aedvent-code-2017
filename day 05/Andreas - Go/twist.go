package main

import "fmt"

func main(){
	input := []int{0,3,0,1,-3}
	pos := 0

	fmt.Println( doStep(&input, pos, 0) )
}


func doStep(ptr *[]int, pos int, ctr int)int {


	input := *ptr

	fmt.Println(pos, input, ctr)
	if pos >= len(input){
		return ctr+1
	} else if input[pos] == 0 {
		input[pos]++
	}

	newPos := pos + input[pos]

	if pos < len(input) {
		input[pos]++
		return doStep(ptr, newPos, ctr+1)
	}

	return ctr + 1
}