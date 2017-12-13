package main

import (
	"bufio"
	"fmt"
	"os"
)

func check(e error) {
	if e != nil {
		panic(e)
	}
}

type Group struct {
	groupScore int
	groupText  string
}

type Garbage struct {
	garbageText string
}

func main() {
	input := readLine("input.txt")

	groups1a := countGarbageScore(calculateGroups("{}"))
	groups2a := countGarbageScore(calculateGroups("<random characters>"))
	groups3a := countGarbageScore(calculateGroups("<<<<>"))
	groups4a := countGarbageScore(calculateGroups("<{!>}>"))
	groups5a := countGarbageScore(calculateGroups("<!!>"))
	groups6a := countGarbageScore(calculateGroups("<!!!>>"))
	groups8a := countGarbageScore(calculateGroups("<{o\"i!a,<{i<a>"))

	fmt.Println("groups1", groups1a)
	fmt.Println("groups2", groups2a)
	fmt.Println("groups3", groups3a)
	fmt.Println("groups4", groups4a)
	fmt.Println("groups5", groups5a)
	fmt.Println("groups6", groups6a)
	fmt.Println("groups8", groups8a)

	fmt.Println("res", countGarbageScore(calculateGroups(input)))

}

func countGarbageScore(garbages Slice) int {
	sum := 0
	for _, s := range garbages {
		garbageRef := s.(*Garbage)
		sum += len(garbageRef.garbageText)
	}
	return sum
}

func calculateGroups(input string) Slice {
	garbages := Slice(make([]Item, 0, 10))

	strs := ""
	garbageText := ""
	groupsOpen := 0
	garbageOpen := false
	negates := false

	for _, c := range input {
		if garbageOpen && !negates && c != rune('>') && c != rune('!') {
			garbageText += string(c)
		}
		if !negates {
			switch c {
			case rune('{'):
				if !garbageOpen {
					groupsOpen++
					strs += string(c)
				}
			case rune('}'):
				if !garbageOpen {
					groupsOpen--
					strs += string(c)
					strs = ""
				}
			case rune('<'):
				garbageOpen = true
			case rune('>'):
				garbages.Append(&Garbage{garbageText})
				garbageOpen = false
				garbageText = ""
			default:

			}
		}
		if !negates && c == rune('!') {
			negates = true
		} else {
			negates = false
		}
	}
	return garbages
}

func readLine(path string) string {
	file, err := os.Open(path)
	check(err)
	defer file.Close()
	scanner := bufio.NewScanner(file)
	for scanner.Scan() {
		return scanner.Text()
	}
	return ""
}
