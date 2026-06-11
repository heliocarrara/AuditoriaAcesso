import React, { useState } from 'react';
import { api } from '../services/api';

interface UsuarioFormProps {
    onUsuarioCadastrado: () => void;
}

export const UsuarioForm: React.FC<UsuarioFormProps> = ({ onUsuarioCadastrado }) => {
    const [nome, setNome] = useState('');
    const [email, setEmail] = useState('');
    const [senha, setSenha] = useState('');
    const [erro, setErro] = useState('');

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setErro('');

        if (!nome || !email || !senha) {
            setErro('Todos os campos são obrigatórios.');
            return;
        }

        try {
            await api.post('/usuarios', { nome, email, senha });

            setNome('');
            setEmail('');
            setSenha('');
            onUsuarioCadastrado();
            alert('Usuário cadastrado com sucesso!');
        } catch (err: any) {
            const mensagemErro = err.response?.data?.mensagem || 'Erro ao cadastrar usuário.';
            setErro(mensagemErro);
        }
    };

    return (
        <div style={{ padding: '20px', border: '1px solid #ccc', borderRadius: '8px', marginBottom: '20px' }}>
            <h3>Cadastrar Novo Usuário</h3>
            {erro && <p style={{ color: 'red' }}>{erro}</p>}
            <form onSubmit={handleSubmit} style={{ display: 'flex', flexDirection: 'column', gap: '10px', maxWidth: '300px' }}>
                <input type="text" placeholder="Nome" value={nome} onChange={e => setNome(e.target.value)} />
                <input type="email" placeholder="E-mail" value={email} onChange={e => setEmail(e.target.value)} />
                <input type="password" placeholder="Senha" value={senha} onChange={e => setSenha(e.target.value)} />
                <button type="submit">Cadastrar</button>
            </form>
        </div>
    );
};