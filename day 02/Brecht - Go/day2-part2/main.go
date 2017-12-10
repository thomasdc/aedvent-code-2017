package main

import (
	"encoding/csv"
	"fmt"
	"math"
	"os"
	"strconv"
)

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func min(arr []int) int {
	m := math.MaxInt64
	for _, e := range arr {
		if e < m {
			m = e
		}
	}
	return m
}

func max(arr []int) int {
	m := 0
	for _, e := range arr {
		if e > m {
			m = e
		}
	}
	return m
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

func transformToInts(records [][]string) [][]int {
	newMatrix := make([][]int, len(records), len(records))
	for i, record := range records {
		newRecord := make([]int, len(record), len(record))
		for j, cellContent := range record {
			v, err := strconv.Atoi(cellContent)
			check(err)
			newRecord[j] = v
		}
		newMatrix[i] = newRecord
	}
	return newMatrix
}

func findDivisableByAndNotEqual(cell int, record []int) int {
	for _, otherCell := range record {
		if otherCell%cell == 0 && otherCell != cell {
			return otherCell
		}
	}
	return -1 // assume no negative numbers
}

func findTwoDivisable(record []int) (int, int) {
	for _, cell := range record {
		otherCell := findDivisableByAndNotEqual(cell, record)
		if otherCell >= 0 {
			return cell, otherCell
		}
	}
	panic("on-mo-ge-lijk")
}

func checksum(filePath string) {
	stringRecords := readFile(filePath)
	intRecords := transformToInts(stringRecords)
	sum := 0
	for _, record := range intRecords {
		v1, v2 := findTwoDivisable(record)
		fmt.Println("------------")
		fmt.Println(v2, v1)
		fmt.Println(v2 / v1)
		sum += (v2 / v1)
	}
	fmt.Println(sum)

}

func main() {
	checksum("input.tsv")
}
