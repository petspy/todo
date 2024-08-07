import { fireEvent, render, screen, waitFor } from '@testing-library/react'

import axiosInstance from '../../api/axios'
import TodoItemAction from './TodoItemAction'

jest.mock('../../api/axios')

describe('TodoItemAction', () => {
  test('Add item should make an API POST request, clear the description upon successful addition, and invoke the onAdd handler', async () => {
    const onAddMock = jest.fn()
    const responseMock = { data: { id: 1, description: 'Test Item', completed: false } }
    axiosInstance.post.mockResolvedValueOnce(responseMock)

    render(<TodoItemAction onAdd={onAddMock} />)

    const descriptionInput = screen.getByPlaceholderText('Enter description...')
    const addButton = screen.getByRole('button', { name: 'Add Item' })

    fireEvent.change(descriptionInput, { target: { value: 'Test Item' } })
    fireEvent.click(addButton)

    await waitFor(() => {
      expect(axiosInstance.post).toHaveBeenCalledWith('/todoitems', expect.any(Object))
    })

    await waitFor(() => {
      expect(descriptionInput.value).toBe('')
    })

    await waitFor(() => {
      expect(onAddMock).toHaveBeenCalledWith(responseMock.data)
    })
  })

  test('Add item should make an API POST request and retain the description if an error occurs', async () => {
    const errorMock = 'Error message'
    axiosInstance.post.mockRejectedValueOnce({ response: { data: errorMock } })

    render(<TodoItemAction />)

    const descriptionInput = screen.getByPlaceholderText('Enter description...')
    const addButton = screen.getByRole('button', { name: 'Add Item' })

    fireEvent.change(descriptionInput, { target: { value: 'Test Item' } })
    fireEvent.click(addButton)

    await waitFor(() => {
      expect(axiosInstance.post).toHaveBeenCalledWith('/todoitems', expect.any(Object))
    })

    await waitFor(() => {
      expect(descriptionInput.value).toBe('Test Item')
    })

    await waitFor(() => {
      expect(screen.getByText(errorMock)).toBeInTheDocument()
    })
  })
})
