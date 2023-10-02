import React, { useState } from "react";
import axios from "axios";
import { FormControl, FormLabel, Button, Input, Box, Center } from '@chakra-ui/react'
import { useNavigate } from "react-router-dom";

function CustomerLogin() {

    let navigate = useNavigate();

    const [loginForm, setLoginForm] = useState({
        email: "",
        password: ""
    });

    const onChangeLoginForm = (e) => {
        setLoginForm({
            ...loginForm,
            [e.target.name]: e.target.value
        })
    }

    const handleSubmit = (e) => {

        e.preventDefault();

        console.log(loginForm);

        var data = JSON.stringify({
            "email": loginForm.email,
            "password": loginForm.password
        });

        const config = {
            method: 'post',
            url: 'https://localhost:44347/api/auth/login',
            headers: {
                'Content-Type': 'application/json',
                "Access-Control-Allow-Origin": "*"
            },
            data: data
        };

        axios(config)
            .then(function () {
                navigate("/Home");
            })
            .catch(function () {
                setLoginForm({
                    ...loginForm,
                    password: ""
                })
            });
    }

    return (
        <>
            <Center>
                <Box maxW='sm' borderWidth='1px' borderRadius='lg' overflow='hidden'>
                    <FormControl>
                        <FormLabel>Email address</FormLabel>
                        <Input type='email' name="email" onChange={onChangeLoginForm} value={loginForm.email} />
                        <FormLabel>Password</FormLabel>
                        <Input type='password' name="password" onChange={onChangeLoginForm} value={loginForm.password} />
                        <Button colorScheme='blue' onClick={handleSubmit}>Login</Button>
                    </FormControl>
                </Box>
            </Center>
        </>
    )
};

export default CustomerLogin;