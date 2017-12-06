
def distribute(block_input):
    m = max(block_input)
    (j, i) = min((v, i) for i, v in enumerate(block_input) if v == m)
    block_input[i] = 0
    l = len(block_input)
    while j > 0:
        i+=1
        block_input[(i) % l] += 1
        j -=1
    return block_input


def block_rotate(block):
    block_result = []
    block_steps = 0
    new = True
    while new:
        block = distribute(block)
        if block not in block_result:
            block_result.append(list(block))
            new = True
            block_steps +=1
        else:
            new = False
    print(block_steps, (len(block_result) - block_result.index(block)))

# block = [0, 2, 7 , 0]
# block_rotate(block)
block = [0,	5,	10,	0,	11,	14,	13,	4,	11,	8,	8,	7,	1,	4,	12,	11]
block_rotate(block)
