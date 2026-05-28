import os
import re

# You are RUNNING from Assets/
ROOT = "."

KEYWORD = "converted"


# ----------------------------
# FIX static readonly -> property getter
# ----------------------------
static_pattern = re.compile(
    r"public\s+static\s+readonly\s+(\w+)\s+(\w+)\s*=\s*(.*?);"
)

def fix_static_readonly(code):
    def repl(m):
        typ = m.group(1)
        name = m.group(2)
        value = m.group(3).strip()

        return f"public {typ} {name} => {value};"

    return static_pattern.sub(repl, code)


def fix_code(code):
    return fix_static_readonly(code)


# ----------------------------
# PROCESS FILES
# ----------------------------
def process_file(path):
    with open(path, "r", encoding="utf-8") as f:
        original = f.read()

    fixed = fix_code(original)

    if fixed != original:
        with open(path, "w", encoding="utf-8") as f:
            f.write(fixed)
        print("✔ FIXED:", path)
    else:
        print("— NO CHANGE:", path)


# ----------------------------
# RUN
# ----------------------------
def run():
    print("Scanning from:", os.path.abspath(ROOT))

    found = 0
    matched = 0

    for root, _, files in os.walk(ROOT):
        for file in files:
            if not file.endswith(".cs"):
                continue

            found += 1
            full_path = os.path.join(root, file)

            print("FOUND:", full_path)

            # filter
            if KEYWORD not in file.lower():
                continue

            matched += 1
            process_file(full_path)

    print("\nSUMMARY:")
    print("CS FILES FOUND:", found)
    print("MATCHED:", matched)


if __name__ == "__main__":
    run()