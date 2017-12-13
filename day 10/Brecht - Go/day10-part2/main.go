package main

import (
	"fmt"
)

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func main() {
	numbersLength := 256
	//	input := "AoC 2017"
	input := "88,88,211,106,141,1,78,254,2,111,77,255,90,0,54,205"

	lengths := StringSlice(make([]int, len(input), len(input)+1))
	for i, n := range input {
		lengths[i] = int(n)
	}
	for _, n := range []int{17, 31, 73, 47, 23} {
		lengths.Append(n)
	}
	numbers := StringSlice(make([]int, 0, 10))
	for i := 0; i < numbersLength; i++ {
		numbers.Append(i)
	}

	curPos := 0
	skip := 0
	for i := 0; i < 64; i++ {
		skip, curPos = hash(numbers, lengths, skip, curPos)
	}

	hashStr := ""
	for i := 0; i < len(numbers)/16; i++ {
		sl := numbers[i*16 : min(i*16+16, len(numbers))]
		res := xorNumbers(sl)
		hex := fmt.Sprintf("%x", res)
		hashStr += hex
	}
	fmt.Println(hashStr)
}

func xorNumbers(numbers []int) int {
	res := 0
	for _, el := range numbers {
		res = res ^ el
	}
	return res
}

func hash(numbers StringSlice, lengths StringSlice, skipSize int, currentPosition int) (int, int) {

	for _, l := range lengths {
		reverse(currentPosition, numbers, l)
		currentPosition = (currentPosition + l + skipSize) % len(numbers)
		skipSize += 1
	}

	return skipSize, currentPosition
}

func reverse(pos int, numbers StringSlice, l int) {
	current1 := pos
	current2 := (pos + l - 1)

	for current2 > current1 {
		i1 := current1 % len(numbers)
		i2 := current2 % len(numbers)

		smallest := numbers[i1]
		biggest := numbers[i2]
		numbers[i2] = smallest
		numbers[i1] = biggest

		current1++
		current2--
	}
}

func min(a, b int) int {
	if a < b {
		return a
	}
	return b
}

func max(a, b int) int {
	if a > b {
		return a
	}
	return b
}
