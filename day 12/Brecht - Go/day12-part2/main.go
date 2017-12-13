package main

import (
	"bufio"
	"fmt"
	"os"
	"regexp"
	"sort"
	"strconv"
	"strings"
	"unicode"
)

type Pipe struct {
	from int
	to   []int
}

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func (s Slice) Len() int {
	return len(s)
}

func (s Slice) Swap(i, j int) {
	s[i], s[j] = s[j], s[i]
}
func (s Slice) Less(i, j int) bool {
	return s[i].(int) < s[j].(int) // very dirty.. but mehh
}

func main() {
	pipeMap := readAll("input.txt")
	allIDs := make(map[int]bool)
	for to, froms := range pipeMap {
		allIDs[to] = true
		for _, from := range froms {
			allIDs[from] = true
		}
	}
	input := Slice(make([]Item, 0, 10))
	input.Append(0)

	// kheb geen zin vandaag.. bruuuuuute foooorce and ugly ftw
	groupsSeen := make(map[string]bool)

	for id, _ := range allIDs {
		s := Slice(make([]Item, 0, 10))
		s.Append(id)
		group := findGroup(s, pipeMap)
		sort.Sort(Slice(group))

		groupKey := strings.Trim(strings.Join(strings.Fields(fmt.Sprint(group)), ","), "[]")
		groupsSeen[groupKey] = true
	}
	fmt.Println(len(groupsSeen))
}

func findNextSet(from int, pipeMap map[int][]int) Slice {
	next := Slice(make([]Item, 0, 10))
	toSet := pipeMap[from]
	for _, to := range toSet {
		next.Append(to)
	}
	return next
}

// hacky as hell but I'm so tired :/
func findGroup(input Slice, pipeMap map[int][]int) Slice {
	visited := make(map[int]int)
	for _, in := range input {
		visited[in.(int)] = 1
	}
	group := input

	foundNew := true
	for foundNew == true {
		foundNew = false
		for _, el := range input {
			if visited[el.(int)] < 2 { // 2 means taken as input
				nextSet := findNextSet(el.(int), pipeMap)
				for _, nextEl := range nextSet {
					if visited[nextEl.(int)] == 0 {
						group.Append(nextEl)
						foundNew = true
						visited[nextEl.(int)] = 1
					}
				}
			}
			visited[el.(int)] = visited[el.(int)] + 1

		}
		input = group
	}
	return group
}

func readAll(path string) map[int][]int {

	m := make(map[int][]int)
	file, err := os.Open(path)
	check(err)
	defer file.Close()
	scanner := bufio.NewScanner(file)
	for scanner.Scan() {
		t := scanner.Text()
		if len(t) > 0 {
			from, to := parse(t)
			m[from] = to
		}
	}

	return m
}

func parse(s string) (int, []int) {
	//2 <-> 0, 3, 4
	re1, err := regexp.Compile(`([\d]+) <-> (([\d]+,? ?)+)`)
	check(err)
	r := re1.FindStringSubmatch(s)
	from, err := strconv.Atoi(r[1])
	check(err)
	toString := strings.Split(r[2], ",")
	toList := make([]int, len(toString), len(toString))
	for i, to := range toString {
		toNumb, err := strconv.Atoi(stripWhitespace(to))
		check(err)
		toList[i] = toNumb
	}
	return from, toList
}

func stripWhitespace(s string) string {
	return strings.Map(func(r rune) rune {
		if unicode.IsSpace(r) {
			return -1
		}
		return r
	}, s)
}
