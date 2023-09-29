import './App.css';
import React from "react";
import { Route, Routes } from "react-router-dom";
import CustomerLogin from './pages/CustomerLogin';
import { ChakraProvider } from '@chakra-ui/react'
import Home from './pages/Home';

function App() {
  return (
    <>
      <ChakraProvider>
        <Routes>
          <Route path="/" element={<CustomerLogin />} />
          <Route path="/Home" element={<Home />} />
        </Routes>
      </ChakraProvider>
    </>
  );
}

export default App;
