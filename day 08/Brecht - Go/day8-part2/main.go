package main

import (
	"bufio"
	"fmt"
	"os"
	"regexp"
	"strconv"

	"github.com/apaxa-go/eval"
)

const MaxInt = int(^uint(0) >> 1)
const MinInt = -(MaxInt - 1)

type Expression struct {
	target             string
	operation          string
	delta              int
	conditionTarget    string
	conditionOperation string
	conditionValue     int
}

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func main() {
	exps := readAll("input.txt")
	env := make(map[string]int)
	uberMax := MinInt
	for _, exp := range exps {
		if evalcondition(env[exp.conditionTarget], exp.conditionValue, exp.conditionOperation) {
			switch exp.operation {
			case "inc":
				env[exp.target] = env[exp.target] + exp.delta
			case "dec":
				env[exp.target] = env[exp.target] - exp.delta
			default:
				panic("PANIEK: ")
			}
			currentMax := getMax(env)
			if currentMax > uberMax {
				uberMax = currentMax
			}
		}
	}
	getMax(env)
	fmt.Println(uberMax, env)
}

func getMax(env map[string]int) int {
	max := MinInt
	for _, value := range env {
		if value > max {
			max = value
		}
	}
	return max
}

func evalcondition(v1 int, v2 int, cond string) bool {
	exprStr := strconv.Itoa(v1) + " " + cond + " " + strconv.Itoa(v2)

	expr, err := eval.ParseString(exprStr, "")
	check(err)
	r, err := expr.EvalToInterface(nil)
	check(err)
	return r.(bool)
}

func readAll(path string) ExpressionSlice {
	file, err := os.Open(path)
	check(err)
	defer file.Close()
	s := ExpressionSlice(make([]Expression, 0, 10))
	scanner := bufio.NewScanner(file)
	for scanner.Scan() {
		t := scanner.Text()
		if len(t) > 0 {
			(&s).Append(parse(t))
		}
	}

	return s
}

func parse(s string) Expression {
	re1, err := regexp.Compile(`([a-z]+) (inc|dec) (-?[\d]+) if ([a-z]+) (>|<|=|>=|<=|==|!=) (-?[\d]+)`)
	check(err)
	r := re1.FindStringSubmatch(s)

	delta, err := strconv.Atoi(r[3])
	check(err)
	conditionValue, err := strconv.Atoi(r[6])

	check(err)
	return Expression{r[1], r[2], delta, r[4], r[5], conditionValue}
}
