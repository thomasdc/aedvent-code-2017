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
		if valid {
			sum += 1
		}
	}
	return sum

}

func checkValidPassword(s string) bool {
	re := regexp.MustCompile("^([a-z ]+)$")
	return re.MatchString(s) && checkNonRepeating(s) && checkNoAnnagrams(s)
}

func checkNonRepeating(s string) bool {
	words := strings.Fields(s)
	wordCounts := wordCount(words)
	for _, word := range words {
		if wordCounts[word] > 1 {
			return false
		}
	}
	return true
}

func checkNoAnnagrams(strs string) bool {
	// overkill but fun.. :)
	words := strings.Fields(strs)
	bitArrayCount := bitArrayCount(words)
	for _, word := range words {
		bitArray := wordToBitArray(word)
		if bitArrayCount[bitArray] > 1 {
			return false
		}
	}
	return true
}

func bitArrayCount(strs []string) map[[400]bool]int {
	dict := make(map[[400]bool]int)
	for _, str := range strs {
		bitArray := wordToBitArray(str)
		_, present := dict[bitArray]
		if present {
			dict[bitArray]++
		} else {
			dict[bitArray] = 1
		}
	}
	return dict
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

func wordToBitArray(s string) [400]bool {
	bitArray := [400]bool{}
	for _, byteC := range s {
		bitArray[byteC] = true
	}
	return bitArray
}
