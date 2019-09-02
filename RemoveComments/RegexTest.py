import re


def initialize_regex(regex_pattern):
    regex = re.compile(regex_pattern, re.MULTILINE)

    return regex


def file_read(path):
    f = open(path, "r")

    text = f.read()

    f.close()

    return text


def run():
    re_find_all_blank_lines = r"^(\s*\r\n){2,}"

    # Initialize the regex patterns one time and reuse the object
    regex = initialize_regex(re_find_all_blank_lines)

    file = r"J:\Dump\TestClass.cs"

    # Using source paths - get all C# files
    text = file_read(file)

    print(regex.search(text))
    # print(regex.match(text))

    # Clean up the rest of the file by removing all of the stray blank lines
    result = regex.sub(r"\r\n", text)

    # Write the replacement file
    print(result)


run()
