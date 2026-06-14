import { useEffect, useState } from 'react';
import { UsuarioForm } from './components/UsuarioForm';
import { UsuarioTabela } from './components/UsuarioTabela';
import { api } from './services/api';
import { Login } from './components/Login';

function App() {
  const [usuarios, setUsuarios] = useState([]);
  const [tela, setTela] = useState<'login' | 'cadastro' | 'dashboard'>('login');
  const [userRole, setUserRole] = useState<string>('User');

  useEffect(() => {
    const token = localStorage.getItem('@AuditoriaAcesso:token');
    const role = localStorage.getItem('@AuditoriaAcesso:role');

    if (token && role) {
      setUserRole(role);
      setTela('dashboard');
      carregarUsuarios();
    }
  }, [tela]);

  const handleLoginSucesso = (role: string) => {
    setUserRole(role);
    setTela('dashboard');
  };

  const handleLogout = () => {
    localStorage.removeItem('@AuditoriaAcesso:token');
    localStorage.removeItem('@AuditoriaAcesso:role');
    setTela('login');
  };

  const carregarUsuarios = async () => {
    try {
      const response = await api.get('/usuarios');
      setUsuarios(response.data);
    } catch (err) {
      console.error('Erro ao buscar usuários:', err);
    }
  };

  return (
    <div style={{ maxWidth: '800px', margin: '0 auto', padding: '20px', fontFamily: 'sans-serif' }}>
      <h1>Sistema de Auditoria de Acesso</h1>
      <hr />
      {
        tela === 'login' && (
          <Login onLoginSucesso={handleLoginSucesso} onIrParaCadastro={() => setTela('cadastro')} />
        )
      }
      {
        tela === 'cadastro' && (
          <UsuarioForm onVoltarParaLogin={() => setTela('login')} />
        )
      }
      {
        tela === 'dashboard' && (
          <div>
            <div style={{ display: 'flex', justifyContent: 'center', marginBottom: '20px' }}>
              <p>Bem-vindo {userRole}</p>
              <button onClick={handleLogout} style={{ backgroundColor: '#ef4444', color: '#fff' }}>Sair</button>
            </div>
            <UsuarioTabela usuarios={usuarios} onUsuarioExcluido={carregarUsuarios} userRole={userRole} />
          </div>
        )
      }
    </div>
  );
}

export default App;