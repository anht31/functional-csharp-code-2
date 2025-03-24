import { Exercises} from './MyArray.js'

let maxSubArray = new Exercises().maxSubArray;

// functionTest seed by array of pair [input, expectedOutput]
function functionTest(testCases) {
  testCases.forEach(([input, expected]) => {
    test(`With input: ${JSON.stringify(input)} output must be: ${expected}`, () => {
      expect(maxSubArray(input)).toBe(expected);
    });
  });
}

describe('Test with func maxSubArray', () => {
  functionTest([
    [[-2,1,-3,4,-1,2,1,-5,4], 6],
    [[1], 1],
    [[5,4,-1,7,8], 23],
  ]);
});
