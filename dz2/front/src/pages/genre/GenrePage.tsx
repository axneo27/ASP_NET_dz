import { useEffect, useState } from "react";
import { Box, Typography, List, ListItem, ListItemText, Paper, IconButton, TextField, Button, CircularProgress } from "@mui/material";
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import SaveIcon from '@mui/icons-material/Save';
import CancelIcon from '@mui/icons-material/Cancel';
import axios from "axios";
import { apiUrl } from "../../env";

interface Genre {
  id: string;
  name: string;
}

const GenrePage = () => {
  const [genres, setGenres] = useState<Genre[]>([]);
  const [loading, setLoading] = useState(true);
  const [newName, setNewName] = useState("");
  const [addLoading, setAddLoading] = useState(false);
  const [editId, setEditId] = useState<string | null>(null);
  const [editName, setEditName] = useState("");
  const [editLoading, setEditLoading] = useState(false);
  const [deleteLoading, setDeleteLoading] = useState<string | null>(null);

  const fetchGenres = () => {
    setLoading(true);
    axios.get(`${apiUrl}/genre`)
      .then(res => setGenres(res.data.payload || []))
      .finally(() => setLoading(false));
  };

  useEffect(fetchGenres, []);

  const handleAdd = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!newName.trim()) return;
    setAddLoading(true);
    await axios.post(`${apiUrl}/genre`, { name: newName });
    setNewName("");
    setAddLoading(false);
    fetchGenres();
  };

  const startEdit = (genre: Genre) => {
    setEditId(genre.id);
    setEditName(genre.name);
  };

  const cancelEdit = () => {
    setEditId(null);
    setEditName("");
  };

  const handleEdit = async (genre: Genre) => {
    if (!editName.trim()) return;
    setEditLoading(true);
    await axios.put(`${apiUrl}/genre`, { id: genre.id, name: editName });
    setEditLoading(false);
    setEditId(null);
    setEditName("");
    fetchGenres();
  };

  const handleDelete = async (id: string) => {
    setDeleteLoading(id);
    await axios.delete(`${apiUrl}/genre`, { params: { id } });
    setDeleteLoading(null);
    fetchGenres();
  };

  return (
    <Box maxWidth={600} mx="auto" my={4}>
      <Typography variant="h4" mb={2}>Жанри</Typography>
      <Paper sx={{ p: 2, mb: 2 }}>
        <form onSubmit={handleAdd} style={{ display: "flex", gap: 8 }}>
          <TextField
            label="Новий жанр"
            value={newName}
            onChange={e => setNewName(e.target.value)}
            size="small"
            required
            sx={{ flex: 1 }}
          />
          <Button type="submit" variant="contained" disabled={addLoading}>
            Додати
          </Button>
          {addLoading && <CircularProgress size={24} />}
        </form>
      </Paper>
      {loading ? (
        <Typography>Завантаження...</Typography>
      ) : (
        <Paper>
          <List>
            {genres.map(g => (
              <ListItem key={g.id} secondaryAction={
                editId === g.id ? (
                  <>
                    <IconButton edge="end" color="primary" onClick={() => handleEdit(g)} disabled={editLoading}>
                      <SaveIcon />
                    </IconButton>
                    <IconButton edge="end" color="inherit" onClick={cancelEdit}>
                      <CancelIcon />
                    </IconButton>
                    {editLoading && <CircularProgress size={24} />}
                  </>
                ) : (
                  <>
                    <IconButton edge="end" color="primary" onClick={() => startEdit(g)}>
                      <EditIcon />
                    </IconButton>
                    <IconButton edge="end" color="error" onClick={() => handleDelete(g.id)} disabled={deleteLoading === g.id}>
                      <DeleteIcon />
                    </IconButton>
                    {deleteLoading === g.id && <CircularProgress size={24} />}
                  </>
                )
              }>
                {editId === g.id ? (
                  <TextField value={editName} onChange={e => setEditName(e.target.value)} size="small" sx={{ minWidth: 120 }} />
                ) : (
                  <ListItemText primary={g.name} />
                )}
              </ListItem>
            ))}
          </List>
        </Paper>
      )}
    </Box>
  );
};

export default GenrePage;
