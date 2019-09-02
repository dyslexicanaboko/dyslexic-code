import re
import glob
import os


def initialize_regex(regex_pattern):
    regex = re.compile(regex_pattern)

    return regex


def file_read(path):
    f = open(path, "r")

    text = f.read()

    f.close()

    return text


def file_write(path, text):
    w = open(path, "w")

    w.write(text)

    w.close()


def process_regex_for_file(regex_comments, regex_blank_lines, target_file):
    # Using source paths - get all C# files
    text = file_read(target_file)

    # Search for a single instance and only proceed if there is an instance found
    if regex_comments.search(text):
        # If something is found then remove all comments on the whole file
        result = regex_comments.sub("", text)

        # Clean up the rest of the file by removing all of the stray blank lines
        result = regex_blank_lines.sub(r"\r\n", result)

        # Write the replacement file
        file_write(target_file, result)


def run(paths_to_search):
    re_find_all_comments = r"(/\*([^*]|[\r\n]|(\*+([^*/]|[\r\n])))*\*+/)|(//.*)"
    re_find_all_blank_lines = r"^(\s*\r\n){2,}"

    # Initialize the regex patterns one time and reuse the object
    regex_comments = initialize_regex(re_find_all_comments)
    regex_blank_lines = initialize_regex(re_find_all_blank_lines)

    for p in paths_to_search:
        # Take the paths that the user provides and append the search string to each path
        search_cs = os.path.join(p, r"**\*.cs")

        # search for all C# files
        lst_files = glob.glob(search_cs, recursive=True)

        for file in lst_files:
            # print(file)

            process_regex_for_file(regex_comments, regex_blank_lines, file)


def test_find_cs_files():
    arr = [
        r"J:\Dev\GitHub\dyslexicanaboko.visualstudio.com\pro-spammer-clean-up\ProMassSpammer.Core",
        r"J:\Dev\GitHub\dyslexicanaboko.visualstudio.com\pro-spammer-clean-up\ProMassSpammer.Data",
        r"J:\Dev\GitHub\dyslexicanaboko.visualstudio.com\pro-spammer-clean-up\ProMassSpammer.Pdf",
        r"J:\Dev\GitHub\dyslexicanaboko.visualstudio.com\pro-spammer-clean-up\ProMassSpammer.Service",
        r"J:\Dev\GitHub\dyslexicanaboko.visualstudio.com\pro-spammer-clean-up\ProMassSpammer.UnitTests",
        r"J:\Dev\GitHub\dyslexicanaboko.visualstudio.com\pro-spammer-clean-up\ProMassSpammer.WindowsService"
    ]

    run(arr)


test_find_cs_files()
