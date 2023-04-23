import React, { Component } from "react";
import { DownloadPDF } from "./components/OpenPDF";
import { Header } from "./components/Header";
import "./custom.css";
import { Layout } from './components/Layout';
import { Route, Routes } from 'react-router-dom';
import AppRoutes from './AppRoutes';

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <div className="App" style={{minHeight: '100%'}}>
        <Header></Header>
        {/* <DownloadPDF /> */}
        <Layout>
            <Routes>
            {AppRoutes.map((route, index) => {
                const { element, ...rest } = route;
                return <Route key={index} {...rest} element={element} />;
            })}
            </Routes>
        </Layout>
      </div>
    );
  }
}
