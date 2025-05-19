function reverseString(str) {
    if (!str) return null
    let result = ''
    for (let i = str.length - 1; i >= 0; i--) {
        result += str[i]
    }
    console.log(result)
    return result
}

reverseString("yoyo master");

function reverseStringRecursive(str) {
    if (!str) return ''

    const nextStr = str.substring(0, str.length - 1)
    return str[str.length - 1] + reverseStringRecursive(nextStr)
}

console.log(reverseStringRecursive("yoyo master"))