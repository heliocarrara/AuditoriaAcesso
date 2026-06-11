import { useEffect, useState } from 'react';
import { UsuarioForm } from './components/UsuarioForm';
import { UsuarioTabela } from './components/UsuarioTabela';
import { api } from './services/api';

function App() {
  const [usuarios, setUsuarios] = useState([]);

  const carregarUsuarios = async () => {
    try {
      const response = await api.get('/usuarios');
      setUsuarios(response.data);
    } catch (err) {
      console.error('Erro ao buscar usuários:', err);
    }
  };

  useEffect(() => {
    carregarUsuarios();
  }, []);

  return (
    <div style={{ maxWidth: '800px', margin: '0 auto', padding: '20px', fontFamily: 'sans-serif' }}>
      <h1>Sistema de Auditoria de Acesso</h1>
      <hr />
      <UsuarioForm onUsuarioCadastrado={carregarUsuarios} />
      <UsuarioTabela usuarios={usuarios} onUsuarioExcluido={carregarUsuarios} />
    </div>
  );
}

export default App;