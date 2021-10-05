const { decode, JWTKeys } = require("./jwt");

test('A malformed JWT string should return generic values', () => {
    let malformedToken = "malformed222";
    let decoded = decode(malformedToken);

    expect(decoded[JWTKeys["role"]]).toBe("Student");
})

test('A properly formed JWT string should return non-generic values', () => {
    // generated from jwt.io, NOT from dev or production server
    let dummyToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IllvbGFuZGEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJOb24tR2VuZXJpYyBSb2xlIiwiaWF0IjoxNTE2MjM5MDIyfQ.LKOFBLpy6-OgNhx6Z4yayZa9vV81mI5Mb_1EkcrQnrU";
    let decoded = decode(dummyToken);

    expect(decoded[JWTKeys["role"]]).toBe("Non-Generic Role");
})