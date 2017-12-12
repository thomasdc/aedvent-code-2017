package main

import (
	"encoding/csv"
	"fmt"
	"os"
	"strconv"
)

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func main() {
	numbersLength := 256
	input := readFile("data.txt")

	//	numbersLength := 5
	//	input := readFile("test.txt")

	lengths := StringSlice(make([]int, len(input), len(input)))
	for i, n := range input {
		val, err := strconv.Atoi(n)
		check(err)
		lengths[i] = val
	}
	numbers := StringSlice(make([]int, 0, 10))
	for i := 0; i < numbersLength; i++ {
		numbers.Append(i)
	}
	hash(numbers, lengths)

}

func hash(numbers StringSlice, lengths StringSlice) {
	skipSize := 0
	currentPosition := 0

	for _, l := range lengths {
		reverse(currentPosition, numbers, l)
		fmt.Println("pos:", currentPosition, numbers, "skip", skipSize, "len", l)
		currentPosition = (currentPosition + l + skipSize) % len(numbers)
		skipSize += 1
	}

	fmt.Println(numbers[0]*numbers[1], numbers)

}

func reverse(pos int, numbers StringSlice, l int) {
	current1 := pos
	current2 := (pos + l - 1)

	fmt.Println(current1, current2)

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

func readFile(filePath string) []string {
	file, err := os.Open(filePath)
	check(err)
	r := csv.NewReader(file)
	r.Comma = ','
	records, err := r.ReadAll()
	check(err)
	return records[0]
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
