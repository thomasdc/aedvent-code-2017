package main

import "fmt"


func main() {
	buffer := part1(2017, 3)	
	fmt.Println(buffer)

	buffer  = part1(2017, 370)	
	fmt.Println(buffer)
}

func part1(iterations, stepsForward int) *[]int{
	
	currPos := 0
	buffer := &[]int{0}
	for i := 1; i <= 2017 ; i++ {

		currPos = (currPos+stepsForward) % len( *buffer )
		currPos++
		
		buffer = insertSlice(buffer, currPos, i)

		//fmt.Println(i, currPos, buffer)	
	}
	return buffer
}
func insertSlice(input *[]int, pos,val int) *[]int{
	orig := *input
	res := make([]int, len(orig)+1)

	for i := 0 ; i < pos ; i++ { res[i] = orig[i] }
	res[pos] = val
	for i := pos ; i < len(orig) ; i++ { res[i+1] = orig[i] }
	
	return &res
}