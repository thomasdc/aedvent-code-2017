package main

type ExpressionSlice []Expression

// Slice Manipulations

func (sRef *ExpressionSlice) DoubleSlice() {
	s := *sRef
	newSlice := make([]Expression, len(s), 2*cap(s))
	copy(newSlice, s)
	*sRef = newSlice
}

func (sRef *ExpressionSlice) Append(value Expression) {

	s := *sRef

	n := len(s)
	if n == cap(s) {
		s.DoubleSlice()
	}

	s = s[0 : n+1]
	s[n] = value
	*sRef = s
}

func (sRef *ExpressionSlice) Contains(e Expression) bool {
	s := *sRef
	for _, a := range s {
		if a == e {
			return true
		}
	}
	return false
}
