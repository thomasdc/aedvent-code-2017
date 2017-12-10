package main

import (
	"encoding/csv"
	"fmt"
	"os"
	"strconv"
	"strings"
)

func main() {
	records := readFile("input.txt")
	record := records[0] // only one record in this case
	hasSeen := make(map[string]bool)

	numbers := make([]int, len(record), len(record))
	for i, el := range record {
		number, err := strconv.Atoi(el)
		check(err)
		numbers[i] = number
	}
	current := numbers
	cycles := 0
	for !hasSeen[createKey(current)] {
		hasSeen[createKey(current)] = true
		current = redistributeToCopy(current, 0)
		cycles += 1
	}
	fmt.Println(cycles)
}

func createKey(s []int) string {
	return strings.Trim(strings.Join(strings.Fields(fmt.Sprint(s)), ","), "[]")
}

func redistributeToCopy(numbers []int, iteration int) []int {
	next := make([]int, len(numbers), len(numbers))
	copy(next, numbers)
	if iteration > 10 {
		panic("")
	}
	largest, i := getLargest(next)
	redistributeLargest(next, largest, i)
	return next
}

func redistributeLargest(numbers []int, val int, index int) {
	numbers[index] = 0
	i := index + 1
	for val > 0 {
		numbers[i%len(numbers)] += 1
		val -= 1
		i += 1
	}
}

func getLargest(record []int) (int, int) {
	max := 0
	index := 0
	for i, el := range record {
		if el > max {
			max = el
			index = i
		}
	}
	return max, index
}

func readFile(filePath string) [][]string {
	file, err := os.Open(filePath)
	check(err)
	r := csv.NewReader(file)
	r.Comma = '\t'
	records, err := r.ReadAll()
	check(err)
	return records
}

func check(e error) {
	if e != nil {
		panic(e)
	}
}
