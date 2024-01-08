
import re
import os

def decode(message_file):
    with open(message_file, 'r') as file:
        encoded_message = file.readlines()
        number_word_map = {}
        for line in encoded_message:
            number, word = line.split()
            number_word_map[int(number)] = word

        encoded_numbers = sorted(number_word_map.keys())

        pyramid = []
        step = 1
        j = 0
        for i in range(1, len(encoded_numbers) + 1):
            j += 1
            if j == step:
                step += 1
                pyramid.append(encoded_numbers[:i])
                j = 0
            

        decoded_message = ""
        for line in pyramid:
            decoded_message += number_word_map[line[-1]] + " "

        return decoded_message.strip()

def main():
    message_file = os.path.realpath(os.path.join(os.getcwd(), os.path.dirname(__file__))) + "//coding_qual_input.txt"
    print(decode(message_file))

if __name__ == "__main__":
    main()

