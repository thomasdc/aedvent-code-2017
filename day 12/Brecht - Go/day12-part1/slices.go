package main

type Item interface{}
type Slice []Item

// Slice Manipulations

func (sRef *Slice) DoubleSlice() {
	s := *sRef
	newSlice := make([]Item, len(s), 2*cap(s))
	copy(newSlice, s)
	*sRef = newSlice
}

func (sRef *Slice) Append(value Item) {

	s := *sRef

	n := len(s)
	if n == cap(s) {
		s.DoubleSlice()
	}

	s = s[0 : n+1]
	s[n] = value
	*sRef = s
}

func (sRef *Slice) Contains(e Item) bool {
	s := *sRef
	for _, a := range s {
		if a == e {
			return true
		}
	}
	return false
}
