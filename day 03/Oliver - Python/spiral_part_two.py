"""
--- Part Two ---

As a stress test on the system, the programs here clear the grid and then store
the value 1 in square 1. Then, in the same allocation order as shown above, they
store the sum of the values in all adjacent squares, including diagonals.

So, the first few squares' values are chosen as follows:

    Square 1 starts with the value 1.

    Square 2 has only one adjacent filled square (with value 1), so it also
    stores 1.

    Square 3 has both of the above squares as neighbors and stores the sum of
    their values, 2.

    Square 4 has all three of the aforementioned squares as neighbors and stores
    the sum of their values, 4.

    Square 5 only has the first and fourth squares as neighbors, so it gets the
    value 5.

Once a square is written, its value does not change. Therefore, the first few
squares would receive the following values:

147  142  133  122   59
304    5    4    2   57
330   10    1    1   54
351   11   23   25   26
362  747  806--->   ...

What is the first value written that is larger than your puzzle input?
"""

# Center piece
a = {(0, 0): 1}
# Directions
i = 0
j = 0

# Values of hor direction
for hor in range(i-1,i+2):
    print(hor)
# Values of ver directions
for ver in range(j-1, j+2):
    print(ver)

# Check surrounding of a i,j coordinate and summarise
# Search for existing adjacent squares, if it doesn't exist in dict a -> throw 0 value
sum_surrounding = lambda i,j: sum(a.get((hor, ver),0) for hor in range(i-1, i+2) for ver in range(j-1, j+2))

step = count(1,2)
a, i, j = {(0,0) : 1}, 0, 0
for s in step:
    if s > 10:
        break
    else:
        for _ in range(s):   i += 1; a[i, j] = sum_surrounding(i, j)
        for _ in range(s):   j += 1; a[i, j] = sum_surrounding(i, j)
        for _ in range(s + 1): i -= 1; a[i, j] = sum_surrounding(i, j)
        for _ in range(s + 1): j -= 1; a[i, j] = sum_surrounding(i, j)


values=list(a.values())
yl = [x > 347991 for x in values]
result = [x for x, y in zip(values, yl) if y == True][0]
result

# a = (0, 0)
# 1 0 for s1 = (1, 0 )
# 1 0 for s2 = (1, 1)
# 1 0 for 1 s+1 = (0, 1)
# 1 1 for 1 s+1 = (-1, 1)
# 1 0 for 2 s+1 = (-1, 0)
# 1 1 for 2 s+1 = (-1, -1)
# 3 0 for s1 = (0, -1)
# 3 1 for s1 = (1, -1)
# 3 2 for s1 = (2, -1)
# 3 0 for s2 = (2, 0)
# 3 1 for s2 = (2, 1)
