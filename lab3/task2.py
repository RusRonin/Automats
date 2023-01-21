import sys
from common import *


if __name__ == "__main__":
    expected_arg_count = 3
    if len(sys.argv) != expected_arg_count:
        print('incorrect argument count')
        sys.exit()
    input = numpy.genfromtxt(sys.argv[1], delimiter=';', dtype='<U25')
    output = to_determined_machine(input)
    numpy.savetxt(sys.argv[2], output, delimiter=';', fmt='%s')
