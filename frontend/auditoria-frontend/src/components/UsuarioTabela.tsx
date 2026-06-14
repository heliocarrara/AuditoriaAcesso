import React from 'react';
import { api } from '../services/api';

interface Usuario {
    id: number;
    nome: string;
    email: string;
    qtdLogsAcesso: number;
}

interface UsuarioTabelaProps {
    usuarios: Usuario[];
    onUsuarioExcluido: () => void;
    userRole: string;
}

export const UsuarioTabela: React.FC<UsuarioTabelaProps> = ({ usuarios, onUsuarioExcluido, userRole }) => {

    const handleExcluir = async (id: number) => {
        if (!confirm('Deseja realmente excluir este usuário?')) return;

        try {
            await api.delete(`/usuarios/${id}`);

            alert('Usuário removido com sucesso.');
            onUsuarioExcluido();
        } catch (err: any) {
            alert(err.message);
        }
    };

    return (
        <div>
            <h3>Usuários Cadastrados</h3>
            <table border={1} cellPadding={8} style={{ width: '100%', borderCollapse: 'collapse', textAlign: 'left' }}>
                <thead>
                    <tr style={{ backgroundColor: '#f2f2f2' }}>
                        <th>Nome</th>
                        <th>E-mail</th>
                        <th>Logs de Acesso</th>
                        {userRole === 'Admin' && <th>Ações</th>}
                    </tr>
                </thead>
                <tbody>
                    {usuarios.map(u => (
                        <tr key={u.id}>
                            <td>{u.nome}</td>
                            <td>{u.email}</td>
                            <td>{u.qtdLogsAcesso}</td>
                            {
                                userRole === 'Admin' &&
                                <td>
                                    <button onClick={() => handleExcluir(u.id)} style={{ color: 'red' }}>Excluir</button>
                                </td>
                            }
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};