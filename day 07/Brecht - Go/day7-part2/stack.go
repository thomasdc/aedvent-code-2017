package main

type StackNode interface{}

// CreateStack returns a new stack.
func CreateStack() *Stack {
	return &Stack{}
}

// Stack is a basic LIFO stack that resizes as needed.
type Stack struct {
	nodes []StackNode
	count int
}

// Push adds a node to the stack.
func (s *Stack) Push(n StackNode) {
	s.nodes = append(s.nodes[:s.count], n)
	s.count++
}

func (s *Stack) Size() int {
	return s.count
}

// Pop removes and returns a node from the stack in last to first order.
func (s *Stack) Pop() StackNode {
	if s.count == 0 {
		return nil
	}
	s.count--
	return s.nodes[s.count]
}
