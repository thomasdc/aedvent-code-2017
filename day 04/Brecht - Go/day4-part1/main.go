package main

import (
	"bufio"
	"fmt"
	"os"
	"regexp"
	"strings"
)

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func main() {
	result := checkValidOnFile("data.txt", checkValidPassword)
	fmt.Println(result)
}

func checkValidOnFile(p string, validFun func(string) bool) int {
	file, err := os.Open(p)
	if err != nil {
		panic(err)
	}
	defer file.Close()

	sum := 0
	scanner := bufio.NewScanner(file)
	for scanner.Scan() {
		valid := validFun(scanner.Text())
		fmt.Println(valid)
		if valid {
			sum += 1
		}
	}
	return sum

}

func checkValidPassword(s string) bool {
	re := regexp.MustCompile("^([a-z ]+)$")
	validStrings := re.MatchString(s)
	return validStrings && checkNonRepeating(s)
}

func checkNonRepeating(s string) bool {
	words := strings.Fields(s)
	wordCount := wordCount(words)
	for _, word := range words {
		if wordCount[word] > 1 {
			return false
		}
	}
	return true
}

func wordCount(s []string) map[string]int {
	dict := make(map[string]int)
	for _, string := range s {
		_, present := dict[string]
		if present {
			dict[string]++
		} else {
			dict[string] = 1
		}
	}
	return dict
}
