# REGEXP in python see module re
# https://docs.python.org/3.6/library/re.html
# This needs a non greedy regexp .*?
# Garbage = everything between < >: : \<.* \>
# Group = everything between { }: \{.* \}
# Ignored = ! +1char \!.

import re

def read_input(path):
    return [l.replace('\n', '') for l in open(path)][0]

def ignore(input):
    return re.sub(r'\!.', '', input)

def garbage_out(input):
    return re.subn(r'\<.*?\>', '', input)

def stream(input):
    stream = (ignore(input))
    clean, garbage_piece = garbage_out(stream)
    depth = 1
    score = 0
    for char in clean:
        if char == '{':
            score += depth
            depth += 1
        elif char == '}':
            depth -= 1
    return score, len(stream) - len(clean) - garbage_piece * 2

# Test unit
def test_answer(input, value):
    groups, garbage = stream(input)
    assert groups == value, "Test ok"
    print("Test has passed!")

test_answer('{<a>,<a>,<a>,<a>}', 1)
test_answer('{{<!!>},{<!!>},{<!!>},{<!!>}}', 9)
test_answer('{{<a!>},{<a!>},{<a!>},{<ab>}}', 3)
test_answer('{}', 1)
test_answer('{{{}}}', 6)
test_answer('{{{},{},{{}}}}', 16)

# check stream
stream(read_input('day 09/Oliver - Python/input.txt'))
