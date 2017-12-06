package main

import (
	"fmt"
	"strconv"
)

func main(){
	
	exampleInput := &[]int{0,2,7,0}
	fmt.Println( Solve1(exampleInput) )

	realInput := &[]int{
		4,  1, 15, 12,
		0,  9,  9,  5,
		5,  8,  7,  3,
		14, 5, 12,  3,
	}
	
	fmt.Println( Solve1(realInput) )
}

func Solve1(memBank *[]int) int{
	steps := 0
	memStates := map[string]bool{}

	//fmt.Println(steps, " => ", *memBank, memStates)
	for _, ex := memStates[key(memBank)] ; !ex ; _, ex = memStates[key(memBank)]{
		
		memStates[key(memBank)] = true
		distribute(memBank, maxMem(memBank))
		
		//fmt.Println(steps, " => ", *memBank, memStates)

		steps++
		//if steps == 26 { panic(nil) }
	}

	return steps
}

func distribute(ptr *[]int, pos int){
	memory := *ptr

	todistr := memory[pos]
	memory[pos] = 0
	pos++
	pos %= len(memory)
	for todistr > 0{

		memory[pos]++
		todistr--

		pos++
		pos %= len(memory)
	}
}

func maxMem(ptr *[]int) int{
	maxPos := 0
	maxVal := (*ptr)[0]
	
	for i, v := range *ptr {
		if v > maxVal{ 
			maxPos, maxVal = i, v 
		} else if v == maxVal && i < maxPos{
			maxPos = i //lowest of the two
		}
	}
	
	return maxPos
}

func key(ptr *[]int) string{
	res := ""
	for _, v := range *ptr{
		res += strconv.Itoa(v) + "-"
	}
	return res
}