package main

type StringSlice []int

// Slice Manipulations

func (sRef *StringSlice) DoubleSlice() {
	s := *sRef
	newSlice := make([]int, len(s), 2*cap(s))
	copy(newSlice, s)
	*sRef = newSlice
}

func (sRef *StringSlice) Append(value int) {
	s := *sRef
	n := len(s)
	if n == cap(s) {
		s.DoubleSlice()
	}
	s = s[0 : n+1]
	s[n] = value
	*sRef = s
}

func (sRef *StringSlice) Contains(e int) bool {
	s := *sRef
	for _, a := range s {
		if a == e {
			return true
		}
	}
	return false
}
