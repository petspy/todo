import React, { useEffect, useState } from 'react'

import { Button, Table } from 'react-bootstrap'

import axiosInstance from '../../api/axios'

const TodoItemTable = ({ onNewItem }) => {
  const [items, setItems] = useState([])
  const [error, setError] = useState('')

  useEffect(() => {
    getItems()
  }, [])

  useEffect(() => {
    if (onNewItem) {
      setItems((prevItems) => [...prevItems, onNewItem])
    }
  }, [onNewItem])

  const getItems = async () => {
    try {
      const response = await axiosInstance.get('/todoitems')
      setItems(response.data)
    } catch (error) {
      setError(error.response?.data ?? error.message)
    }
  }

  const handleMarkAsComplete = async (item) => {
    try {
      await axiosInstance
        .put(`/todoitems/${item.id}`, {
          ...item,
          isCompleted: true,
        })
        .then(() => {
          // Remove item from the list
          setItems((prevItems) => prevItems.filter((i) => i.id !== item.id))
        })
    } catch (error) {
      setError(error.response?.data ?? error.message)
    }
  }

  return (
    <>
      <h1>
        Showing {items.length} Item(s){' '}
        <Button variant="primary" className="pull-right" onClick={getItems}>
          Refresh
        </Button>
      </h1>
      {error && <p className="text-danger">{error}</p>}

      <Table striped bordered hover>
        <thead>
          <tr>
            <th>Id</th>
            <th>Description</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {items.map((item) => (
            <tr key={item.id}>
              <td>{item.id}</td>
              <td>{item.description}</td>
              <td>
                <Button variant="warning" size="sm" onClick={() => handleMarkAsComplete(item)}>
                  Mark as completed
                </Button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
    </>
  )
}

export default TodoItemTable
