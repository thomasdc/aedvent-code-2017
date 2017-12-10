package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
)

func main() {
	inputSlice := readFile("input.txt")
	i := 0
	steps := 0
	for i >= 0 && i < len(inputSlice) {
		relI := inputSlice[i]
		if relI >= 3 {
			inputSlice[i] = relI - 1

		} else {
			inputSlice[i] = relI + 1

		}

		i = i + relI
		steps += 1
	}
	fmt.Println(steps, i)
}

type slice []int

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func readFile(p string) []int {
	input := slice(make([]int, 0, 20))
	file, err := os.Open(p)
	if err != nil {
		panic(err)
	}
	defer file.Close()

	scanner := bufio.NewScanner(file)
	for scanner.Scan() {
		number, err := strconv.Atoi(scanner.Text())
		check(err)
		input.Append(number)
	}
	return input
}

func (sRef *slice) Append(value int) {
	s := *sRef
	n := len(s)
	if n == cap(s) {
		s.DoubleSlice()
	}
	s = s[0 : n+1]
	s[n] = value
	*sRef = s
}

func (sRef *slice) DoubleSlice() {
	s := *sRef
	newSlice := make([]int, len(s), 2*cap(s))
	copied := copy(newSlice, s)
	fmt.Println("copied", copied)
	*sRef = newSlice
}
