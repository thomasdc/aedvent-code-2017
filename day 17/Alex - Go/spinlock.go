package main

import (
	"container/ring"
	"fmt"
)

func main() {
	part1()
	part2()
}

func part1() {
	r := ring.New(1)
	r.Value = 0
	spinlock(r, 2017)
	fmt.Println(r.Next().Value)
}

func spinlock(r *ring.Ring, iterations int) {
	for i := 1; i <= iterations; i++ {
		r = r.Move(301)
		ins := &ring.Ring{Value: i}
		r.Link(ins)
		r = ins
	}
}

func part2() {
	r := ring.New(1)
	r.Value = 0
	zero := r
	spinlock(r, 50000000)
	fmt.Println(zero.Next().Value)
}
