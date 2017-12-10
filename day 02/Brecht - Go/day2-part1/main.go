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

func checksum(filePath string) {
	stringRecords := readFile(filePath)
	intRecords := transformToInts(stringRecords)
	sum := 0
	for _, el := range intRecords {
		minRow := min(el)
		maxRow := max(el)
		diff := maxRow - minRow
		sum += diff
	}
	fmt.Println(sum)

}

func main() {
	checksum("input.tsv")
}
