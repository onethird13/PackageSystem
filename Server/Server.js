const express = require("express");
const app = express();
const port = 9178;

app.use(express.static("public"));

app.listen(port, () => {
    console.log(`Server running at http://localhost:${port}`);
}).on("error", (err) => {
    console.error("Server failed:", err);
});