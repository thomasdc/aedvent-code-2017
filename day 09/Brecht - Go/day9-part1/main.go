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

func main() {
	input := readLine("input.txt")
	groups1 := calculateGroups("{}")
	groups2 := calculateGroups("{{{}}}")
	groups3 := calculateGroups("{{},{}}")
	groups4 := calculateGroups("{{{},{},{{}}}}")
	groups5 := calculateGroups("{<{},{},{{}}>}")
	groups6 := calculateGroups("{<a>,<a>,<a>,<a>}")
	groups7 := calculateGroups("{{<a>},{<a>},{<a>},{<a>}}")
	groups8 := calculateGroups("{{<!>},{<!>},{<!>},{<a>}}")

	fmt.Println("groups1", groups1)
	fmt.Println("groups2", groups2)
	fmt.Println("groups3", groups3)
	fmt.Println("groups4", groups4)
	fmt.Println("groups5", groups5)
	fmt.Println("groups6", groups6)
	fmt.Println("groups7", groups7)
	fmt.Println("groups8", groups8)

	groups1a := countGroupsScore(calculateGroups("{}"))
	groups2a := countGroupsScore(calculateGroups("{{{}}}"))
	groups3a := countGroupsScore(calculateGroups("{{},{}}"))
	groups4a := countGroupsScore(calculateGroups("{{{},{},{{}}}}"))
	groups5a := countGroupsScore(calculateGroups("{<a>,<a>,<a>,<a>}"))
	groups6a := countGroupsScore(calculateGroups("{{<ab>},{<ab>},{<ab>},{<ab>}}"))
	groups7a := countGroupsScore(calculateGroups("{{<!!>},{<!!>},{<!!>},{<!!>}}"))
	groups8a := countGroupsScore(calculateGroups("{{<a!>},{<a!>},{<a!>},{<ab>}}"))

	fmt.Println("groups1", groups1a)
	fmt.Println("groups2", groups2a)
	fmt.Println("groups3", groups3a)
	fmt.Println("groups4", groups4a)
	fmt.Println("groups5", groups5a)
	fmt.Println("groups6", groups6a)
	fmt.Println("groups7", groups7a)
	fmt.Println("groups8", groups8a)

	fmt.Println("res", countGroupsScore(calculateGroups(input)))

}

func countGroupsScore(groups Slice) int {
	sum := 0
	for _, s := range groups {
		groupRef := s.(*Group)
		sum += groupRef.groupScore
	}
	return sum
}

func calculateGroups(input string) Slice {
	groups := Slice(make([]Item, 0, 10))
	strs := ""

	groupsOpen := 0
	garbageOpen := false
	negates := false

	for _, c := range input {
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
					groups.Append(&Group{groupsOpen + 1, strs})
					strs = ""
				}
			case rune('<'):
				garbageOpen = true
			case rune('>'):
				garbageOpen = false
			default:

			}
		}
		if !negates && c == rune('!') {
			negates = true
		} else {
			negates = false
		}
	}
	return groups
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
