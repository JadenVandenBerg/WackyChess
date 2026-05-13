import re
import sys
from pathlib import Path


def convert_coords(text):
    # ----------------------------------------
    # Convert:
    # int[,] -> coords[]
    # ----------------------------------------

    text = re.sub(
        r'int\s*\[\s*,\s*\]',
        'coords[]',
        text
    )

    # ----------------------------------------
    # Convert 2D array initializers
    #
    # {
    #   {1,2},
    #   {3,4}
    # }
    #
    # ->
    #
    # {
    #   new coords(1,2),
    #   new coords(3,4)
    # }
    # ----------------------------------------

    def replace_2d_initializer(match):
        content = match.group(1)

        pairs = re.findall(
            r'\{\s*(-?\d+)\s*,\s*(-?\d+)\s*\}',
            content
        )

        converted = ", ".join(
            f'new coords({x}, {y})'
            for x, y in pairs
        )

        return "{ " + converted + " }"

    text = re.sub(
        r'\{\s*((?:\{\s*-?\d+\s*,\s*-?\d+\s*\}\s*,?\s*)+)\s*\}',
        replace_2d_initializer,
        text
    )

    # ----------------------------------------
    # Convert:
    # int[] -> coords
    # ----------------------------------------

    text = re.sub(
        r'int\s*\[\s*\]',
        'coords',
        text
    )

    # ----------------------------------------
    # Convert:
    # new int[] { x, y }
    # ->
    # new coords(x, y)
    # ----------------------------------------

    text = re.sub(
        r'new\s+int\s*\[\s*\]\s*\{\s*(-?\d+)\s*,\s*(-?\d+)\s*\}',
        r'new coords(\1, \2)',
        text
    )

    # ----------------------------------------
    # Convert:
    # { x, y }
    # ->
    # new coords(x, y)
    #
    # ONLY exact 2-number arrays
    # ----------------------------------------

    text = re.sub(
        r'\{\s*(-?\d+)\s*,\s*(-?\d+)\s*\}',
        r'new coords(\1, \2)',
        text
    )

    # ----------------------------------------
    # ONLY convert:
    #
    # coords startSquare = null
    #
    # ->
    #
    # coords startSquare = new coords(-1, -1)
    # ----------------------------------------

    text = re.sub(
        r'coords\s+startSquare\s*\{\s*get;\s*set;\s*\}\s*=\s*null;',
        'coords startSquare { get; set; } = new coords(-1, -1);',
        text
    )

    return text


if __name__ == "__main__":
    if len(sys.argv) < 2:
        print("Usage:")
        print("python convert_coords.py MyFile.cs")
        sys.exit(1)

    file_path = Path(sys.argv[1])

    original = file_path.read_text(encoding="utf-8")

    converted = convert_coords(original)

    output_path = file_path.with_name(
        file_path.stem + "_converted.cs"
    )

    output_path.write_text(converted, encoding="utf-8")

    print(f"Converted file written to: {output_path}")