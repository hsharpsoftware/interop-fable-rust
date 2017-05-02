var path = require("path");
var webpack = require("webpack");

var cfg = {
  devtool: "source-map",
  entry: "./fable/library.js",
  output: {
    path: path.join(__dirname, "public"),
    filename: "bundle.js",
	// export itself to a global var
	libraryTarget: "var",
	// name of the global var: "Foo"
	library: "FableLib"	
  },
  module: {
    rules: [
      {
        test: /\.js$/,
        //exclude: /node_modules/,
        loader: "source-map-loader"
      },
      {
        test: /\.js$/,
        //exclude: /node_modules/,
        loader: "babel-loader"
      }
    ]
  }  
};

module.exports = cfg;