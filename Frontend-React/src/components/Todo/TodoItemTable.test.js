import { fireEvent, render, screen, waitFor } from '@testing-library/react'

import axiosInstance from '../../api/axios'
import TodoItemTable from './TodoItemTable'

jest.mock('../../api/axios')

describe('TodoItemTable', () => {
  test('Mark an item as complete should call Put API and remove the item from the table', async () => {
    const item = { id: 1, description: 'Test Item', isCompleted: false }
    axiosInstance.get.mockResolvedValue({ data: [item] })
    axiosInstance.put.mockResolvedValueOnce({ data: {} })

    render(<TodoItemTable />)

    await new Promise((resolve) => {
      setTimeout(() => {
        const markAsCompleteButton = screen.getByRole('button', { name: 'Mark as completed' })
        fireEvent.click(markAsCompleteButton)
        resolve()
      }, 500)
    })

    await waitFor(() => {
      expect(axiosInstance.put).toHaveBeenCalledWith(`/todoitems/${item.id}`, {
        ...item,
        isCompleted: true,
      })
    })

    await waitFor(() => {
      expect(screen.queryByText(item.description)).not.toBeInTheDocument()
    })
  })

  test('Mark an item as complete should call PUT API and display any error message on screen', async () => {
    const item = { id: 1, description: 'Test Item', isCompleted: false }
    const errorMock = 'Error message'
    axiosInstance.get.mockResolvedValue({ data: [item] })
    axiosInstance.put.mockRejectedValueOnce({ response: { data: errorMock } })

    render(<TodoItemTable />)

    await new Promise((resolve) => {
      setTimeout(() => {
        const markAsCompleteButton = screen.getByRole('button', { name: 'Mark as completed' })
        fireEvent.click(markAsCompleteButton)
        resolve()
      }, 500)
    })

    await waitFor(() => {
      expect(axiosInstance.put).toHaveBeenCalledWith(`/todoitems/${item.id}`, {
        ...item,
        isCompleted: true,
      })
    })

    await waitFor(() => {
      expect(screen.getByText(errorMock)).toBeInTheDocument()
    })
  })
})
