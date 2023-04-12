import React, { Component } from "react";
import { DownloadPDF } from "./components/DownloadPDF";
import { Header } from "./components/Header";
import "./custom.css";

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <div className="App">
        <Header></Header>
        <DownloadPDF />
        {/* <Layout>
                <Routes>
                {AppRoutes.map((route, index) => {
                    const { element, ...rest } = route;
                    return <Route key={index} {...rest} element={element} />;
                })}
                </Routes>
            </Layout> */}
      </div>
    );
  }
}
