import React, { useState } from "react";
import { api } from "../services/api";

interface LoginProps {
    onLoginSucesso: (role: string) => void;
    onIrParaCadastro: () => void;
}

export const Login: React.FC<LoginProps> = ({ onLoginSucesso, onIrParaCadastro }) => {
    const [email, setEmail] = useState('');
    const [senha, setSenha] = useState('');
    const [erro, setErro] = useState('');

    const handleLogin = async (e: React.FormEvent) => {
        e.preventDefault();
        setErro('');

        if (!email || !senha) {
            setErro('Email e senha são necessários!');
        }

        try {
            const response = await api.post('/usuarios/login', { email, senha });
            const { token } = response.data;

            const payloadBase64 = token.split('.')[1];
            const payloadDecodificado = JSON.parse(atob(payloadBase64));

            const role = payloadDecodificado.role || 'User';

            console.log('Role do usuário:', role);

            localStorage.setItem('@AuditoriaAcesso:token', token);
            localStorage.setItem('@AuditoriaAcesso:role', role);

            onLoginSucesso(role);
            alert('Login realizado com sucesso!');
        } catch (error: any) {
            var erroMessage = error?.response?.data?.message || 'Erro ao fazer login. Verifique suas credenciais.';
            setErro(erroMessage);
        }
    };

    return (
        <div style={{ maxWidth: '400px', margin: '50px auto', padding: '20px' }}>
            <form onSubmit={handleLogin} style={{ display: 'flex', flexDirection: 'column', gap: '12px' }}>
                <h3>Tela de Login</h3>
                {erro && <p style={{ color: 'red', textAlign: 'center', fontSize: '12px' }}>{erro}</p>}
                <input type="email" placeholder="Email" value={email} onChange={(e) => setEmail(e.target.value)} />
                <input type="password" placeholder="Senha" value={senha} onChange={(e) => setSenha(e.target.value)} />
                <button type="submit">Login</button>
                <button type="button" onClick={onIrParaCadastro} style={{ backgroundColor: 'transparent', color: 'blue', border: 'none', textDecoration: 'underline' }}>
                    Ir para Cadastro
                </button>
            </form>
        </div>
    );
}